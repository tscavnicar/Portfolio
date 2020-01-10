import { Component, TemplateRef } from '@angular/core';
import { FieldRecordsClient, CreateFieldRecordCommand, FieldRecordDto, UpdateFieldRecordCommand, FieldVm, FieldListsClient, FieldListDto, CreateFieldListCommand, UpdateFieldListCommand, /*UpdateTodoItemDetailCommand*/ } from '../fieldr-api';
import { faPlus, faEllipsisH } from '@fortawesome/free-solid-svg-icons';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-field-component',
  templateUrl: './field.component.html'
  //styleUrls: ['./field.component.css']
})
export class FieldComponent {

  debug: boolean;

  vm: FieldVm;

  selectedList: FieldListDto;
  selectedItem: FieldRecordDto;

  newListEditor: any = {};
  listOptionsEditor: any = {};
  itemDetailsEditor: any = {};

  newListModalRef: BsModalRef;
  listOptionsModalRef: BsModalRef;
  deleteListModalRef: BsModalRef;
  itemDetailsModalRef: BsModalRef;

  faPlus = faPlus;
  faEllipsisH = faEllipsisH;

  constructor(private listsClient: FieldListsClient, private itemsClient: FieldRecordsClient, private modalService: BsModalService) {
    listsClient.get().subscribe(
      result => {
        this.vm = result;
        if (this.vm.lists.length) {
          this.selectedList = this.vm.lists[0];
        }
      },
      error => console.error(error)
    );
  }

  // Lists
  totalItems(list: FieldListDto): number {
    return list.fieldRecords.length;
  }

  showNewListModal(template: TemplateRef<any>): void {
    this.newListModalRef = this.modalService.show(template);
    setTimeout(() => document.getElementById("title").focus(), 250);
  }

  newListCancelled(): void {
    this.newListModalRef.hide();
    this.newListEditor = {};
  }

  addList(): void {
    let list = FieldListDto.fromJS({
      id: 0,
      name: this.newListEditor.name,
      fieldRecords: [],
    });

    this.listsClient.create(<CreateFieldListCommand>{ name: this.newListEditor.name }).subscribe(
      result => {
        list.id = result;
        this.vm.lists.push(list);
        this.selectedList = list;
        this.newListModalRef.hide();
        this.newListEditor = {};
      },
      error => {
        let errors = JSON.parse(error.response);

        if (errors && errors.Title) {
          this.newListEditor.error = errors.Title[0];
        }

        setTimeout(() => document.getElementById("title").focus(), 250);
      }
    );
  }

  showListOptionsModal(template: TemplateRef<any>) {
    this.listOptionsEditor = {
      id: this.selectedList.id,
      name: this.selectedList.name,
    };

    this.listOptionsModalRef = this.modalService.show(template);
  }

  updateListOptions() {
    this.listsClient.update(this.selectedList.id, UpdateFieldListCommand.fromJS(this.listOptionsEditor))
      .subscribe(
        () => {
          this.selectedList.name = this.listOptionsEditor.name,
            this.listOptionsModalRef.hide();
          this.listOptionsEditor = {};
        },
        error => console.error(error)
      );
  }

  confirmDeleteList(template: TemplateRef<any>) {
    this.listOptionsModalRef.hide();
    this.deleteListModalRef = this.modalService.show(template);
  }

  deleteListConfirmed(): void {
    this.listsClient.delete(this.selectedList.id).subscribe(
      () => {
        this.deleteListModalRef.hide();
        this.vm.lists = this.vm.lists.filter(t => t.id != this.selectedList.id)
        this.selectedList = this.vm.lists.length ? this.vm.lists[0] : null;
      },
      error => console.error(error)
    );
  }

  // Items

  showItemDetailsModal(template: TemplateRef<any>, item: FieldRecordDto): void {
    setTimeout(() => document.getElementById('note').focus(), 100);
    this.selectedItem = item;
    this.itemDetailsEditor = {
      ...this.selectedItem
    };

    this.itemDetailsModalRef = this.modalService.show(template);
  }

  showNewItemDetailsModal(template: TemplateRef<any>): void {
    setTimeout(() => document.getElementById('note').focus(), 100);
    this.itemDetailsEditor = {};      
    this.itemDetailsModalRef = this.modalService.show(template);
  }

  createItemDetails(): void {
       
    this.itemsClient.create(CreateFieldRecordCommand.fromJS({ note: this.itemDetailsEditor.note, listId: this.selectedList.id }))
      .subscribe(
        result => {
          console.log("this.itemDetailsEditor.note:", this.itemDetailsEditor.note);

          this.itemDetailsModalRef.hide();
          
          let item = FieldRecordDto.fromJS({
            id: result,
            listId: this.selectedList.id,
            note: this.itemDetailsEditor.note
          });

          this.selectedList.fieldRecords.push(item);

          this.itemDetailsEditor = {};

        },
        error => console.error(error)
      );
  }

  updateItemDetails(): void {
    this.itemsClient.update(this.selectedItem.id, UpdateFieldRecordCommand.fromJS(this.itemDetailsEditor))
      .subscribe(
        () => {

          console.log(this.selectedItem);

          this.selectedItem.note = this.itemDetailsEditor.note;
          this.itemDetailsModalRef.hide();
          this.itemDetailsEditor = {};

          console.log('Update succeeded.')
        },
        error => console.error(error)
      );
  }

  addItem() {
    let item = FieldRecordDto.fromJS({
      id: 0,
      listId: this.selectedList.id,
      note: ''
    });

    this.selectedList.fieldRecords.push(item);
    let index = this.selectedList.fieldRecords.length - 1;
    this.editItem(item, 'itemTitle' + index);
  }

  editItem(item: FieldRecordDto, inputId: string): void {
    this.selectedItem = item;
    setTimeout(() => document.getElementById(inputId).focus(), 100);
  }

  updateItem(item: FieldRecordDto, pressedEnter: boolean = false): void {
    if (item.id == 0) {
      this.itemsClient.create(CreateFieldRecordCommand.fromJS({ ...item, listId: this.selectedList.id }))
        .subscribe(
          result => {
            item.id = result;
          },
          error => console.error(error)
        );
    } else {
      this.itemsClient.update(item.id, UpdateFieldRecordCommand.fromJS(item))
        .subscribe(
          () => console.log('Update succeeded.'),
          error => console.error(error)
        );
    }

    this.selectedItem = null;
  }

  // Delete item
  deleteItem(item: FieldRecordDto) {
    if (this.itemDetailsModalRef) {
      this.itemDetailsModalRef.hide();
    }

    if (item.id == 0) {
      let itemIndex = this.selectedList.fieldRecords.indexOf(this.selectedItem);
      this.selectedList.fieldRecords.splice(itemIndex, 1);
    } else {
      this.itemsClient.delete(item.id).subscribe(
        () => this.selectedList.fieldRecords = this.selectedList.fieldRecords.filter(t => t.id != item.id),
        error => console.error(error)
      );
    }
  }
}
