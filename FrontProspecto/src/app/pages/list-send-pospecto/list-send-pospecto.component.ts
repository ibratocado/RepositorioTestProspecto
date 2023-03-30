import { Component, OnInit } from '@angular/core';
import { LazyLoadEvent, MessageService } from 'primeng/api';
import { DialogService } from 'primeng/dynamicdialog';
import { DetailsProspectoComponent } from 'src/app/components/details-prospecto/details-prospecto.component';
import { IprospectoRespon } from 'src/app/interfaces/iprospecto-respon';
import { ProspectoService } from 'src/app/services/prospecto.service';

@Component({
  selector: 'app-list-send-pospecto',
  templateUrl: './list-send-pospecto.component.html',
  styleUrls: ['./list-send-pospecto.component.scss'],
  providers: []
})
export class ListSendPospectoComponent implements OnInit {

  public progress = true;
  public propects: IprospectoRespon[] = [];
  public changeUpload:boolean = true;
  constructor(private prospectoService: ProspectoService,
              private messageService: MessageService,
              public dialogService: DialogService) { }

  ngOnInit(): void {
  }

  public loadPospect(event: LazyLoadEvent){
    this.prospectoService.GetFullByState(3).then((data)=>{
      this.propects = data.respon.data;
      setTimeout(() => {
        this.progress = false;
      }, 1000);
      this.messageService.add(
        {key: 'tc',severity: 'success', summary: 'Satisfactorio', detail: data.respon.message});
    }).catch(()=>{
      this.progress = false;
      this.messageService.add(
        {key: 'tc',severity: 'error', summary: 'ERROR', detail: "Se produjo un Error intente mas tarde"});
    });
  }

  public onDetailsProspec(data: any ){
    let id = data;
    this.changeUpload = false;
    this.show(data);
    console.log(id);

  }

  private show(data: any) {
    const ref = this.dialogService.open(DetailsProspectoComponent, {
        header: 'Detalles Prospecto',
        modal: true,
        data: data,
        width: '640px'
    });

    ref.onClose.subscribe((car: any) => {
        this.changeUpload = true;
    });
}
}
