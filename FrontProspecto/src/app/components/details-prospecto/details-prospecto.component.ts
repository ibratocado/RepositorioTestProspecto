import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MessageService } from 'primeng/api';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { IdocumentRespon } from 'src/app/interfaces/idocument-respon';
import { IprospectoRequest } from 'src/app/interfaces/iprospecto-request';
import { IprospectoStatusRequest } from 'src/app/interfaces/iprospecto-status-request';
import { DocumentsService } from 'src/app/services/documents.service';
import { ProspectoService } from 'src/app/services/prospecto.service';

@Component({
  selector: 'app-details-prospecto',
  templateUrl: './details-prospecto.component.html',
  styleUrls: ['./details-prospecto.component.scss']
})
export class DetailsProspectoComponent implements OnInit {

  public observactionActive: boolean = false;
  public enableAutorize: boolean = true;
  public enableReject: boolean = true;
  public changeAutorize?: boolean = true;
  public changeRejetc?: boolean = true;
  public data: any;
  public docs: IdocumentRespon[] = [];
  public formData: FormGroup =  new FormGroup({
    observations: new FormControl('',[Validators.minLength(10),Validators.required])});

  constructor(private messageService: MessageService,
    public ref: DynamicDialogRef,
    public config: DynamicDialogConfig,
    private docService: DocumentsService,
    private prospectoService: ProspectoService) { }

  ngOnInit(): void {
    this.data = this.config.data;
    this.getDoc();
  }

  private getDoc(){
    this.docService.getByProspecto(this.data.id).then((data)=>{
      this.docs = data.respon.data;
    });

  }

  private driveButtonT(){
    this.enableAutorize = true;
    this.enableReject = true;
    this.changeRejetc = true;
    this.changeAutorize = true;
  }
  private driveButtons(){
    this.enableAutorize = false;
    this.enableReject = false;
    this.changeRejetc = false;
    this.changeAutorize = false;
  }
  public onAutorize(){
    let model: IprospectoStatusRequest = {
      id: this.data.id,
      status: 3,
      observations: ""
    };
    this.driveButtons();
    this.prospectoService.putStatus(model).then((data)=>{
      this.messageService.add({severity: 'success', summary: 'Satisfactorio', detail: data.respon.message});
      this.driveButtonT();
      this.ref.close();
    });
  }

  public onReject(){
    this.observactionActive = true;

    console.log(this.changeRejetc);
    //this.driveButtons();
    if(this.changeRejetc)
      return;

    if(!this.formData.valid){
      this.messageService.add(
        {severity:'info', summary:'Info', detail:'Para Rechazar una solicitud debe poner observaciones'});
        return;
    }
    this.driveButtons();
    let model: IprospectoStatusRequest = {
      id: this.data.id,
      status: 3,
      observations: this.formData.get('observations')?.value
    };
    this.driveButtons();
    this.prospectoService.putStatus(model).then((data)=>{
      this.messageService.add({severity: 'success', summary: 'Satisfactorio', detail: data.respon.message});
      this.driveButtonT();
      this.ref.close();
    });
  }
}
