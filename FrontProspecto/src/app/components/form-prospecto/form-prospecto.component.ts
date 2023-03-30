import { Component, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MessageService } from 'primeng/api';
import { Subscription } from 'rxjs';
import { IdocumentRequest } from 'src/app/interfaces/idocument-request';
import { IprospectoRequest } from 'src/app/interfaces/iprospecto-request';
import { DocumentsService } from 'src/app/services/documents.service';
import { ProspectoService } from 'src/app/services/prospecto.service';

@Component({
  selector: 'app-form-prospecto',
  templateUrl: './form-prospecto.component.html',
  styleUrls: ['./form-prospecto.component.scss']
})
export class FormProspectoComponent implements OnInit {

  @ViewChild('fileUpload', {static: false}) fileUpload: any;
  private subForm: Subscription | undefined;

  public uploadedFiles: File[] = [];
  public enableUpload = false;
  public changeUpload = true;
  private errorDocument?:string | undefined;

  public formData: FormGroup =  new FormGroup({
    name: new FormControl('',[Validators.minLength(3),Validators.required]),
    firstLastName: new FormControl('',[Validators.minLength(3),Validators.required]),
    secondLastName: new FormControl('',[Validators.maxLength(10),Validators.required]),
    phone: new FormControl('',[Validators.minLength(10),Validators.required]),
    rfc: new FormControl('',[Validators.required,Validators.minLength(13),Validators.maxLength(13)]),
  });

  public formAddres: FormGroup = new FormGroup({
    stret: new FormControl('',[Validators.required,Validators.minLength(5)]),
    number: new FormControl('',[Validators.required,Validators.max(100000)]),
    city: new FormControl('',[Validators.required,Validators.minLength(5)]),
    postalCode: new FormControl('',[Validators.required,Validators.minLength(5)]),
  });

  constructor(private prospectoService: ProspectoService,
              private documentService: DocumentsService,
              private messageService: MessageService) { }

  ngOnInit(): void {
  }

  public validFormDataControl(name: string): boolean{
    var valid = this.formData.controls[name].valid
    //console.log(name , valid);
    return !valid;
  }

  public validFormAddresControl(name: string): boolean{
    var valid = this.formAddres.controls[name].valid
    //console.log(name , valid);
    return !valid;
  }

  public onUpload(event: any ) {
    for(let file of event.files) {
        this.uploadedFiles.push(file);
    }
    this.fileUpload.clear();
    this.validateFormsAndFile();

    this.messageService.add({key: 'tc',severity: 'info', summary: 'Information', detail: 'Archivos Agregados'});
  }

  private validateFormsAndFile(){
    if(!(this.formAddres.valid && this.formData.valid) || this.uploadedFiles.length == 0){
      this.enableUpload = false;
      return;
    }
    this.enableUpload = true;
  }

  private cleanForm(){
    this.enableUpload = false;
    this.changeUpload = true;
    this.formData.reset();
    this.formAddres.reset();
    this.uploadedFiles = [];
  }

  private erroShow(message: string){
    this.enableUpload = true;
    this.changeUpload = true;
    this.messageService.add(
        {key: 'tc',severity: 'error', summary: 'ERROR', detail: message});
  }

  private mapDocuments(id: any){
    console.log("Map--> ",id.data);
    let request: IdocumentRequest;
    request = id.data;
    this.uploadedFiles.forEach((item)=>{
      request.DocumentData.push(item);
    });

    this.documentService.insertByProspecto(request).then((data)=>{

      this.messageService.add(
        {key: 'tc',severity: 'success', summary: 'Satisfactorio', detail: data.respon.Data});

      this.cleanForm();
    }).catch(()=>{
      this.errorDocument = id;
      this.erroShow("A ocurrido un erros al gregar los documentos");
    });
  }


  public onInsertProspectoAndReset(){
    this.enableUpload = false;

    if(this.errorDocument){
      this.mapDocuments(this.errorDocument);
      return;
    }

    let model = this.mapRequestProspecto();

    this.prospectoService.Post(model).then((data)=>{
      console.log(data.respon);
      let id = data.respon;
      this.mapDocuments(id);
    }).catch(()=>{
      this.erroShow("A ocurrido un erros al gregar el prospecto");
    });

  }

  private mapRequestProspecto(): IprospectoRequest{
    let model: IprospectoRequest = {
      nombre: this.formData.get('name')?.value,
      primerApellido: this.formData.get('firstLastName')?.value,
      segundoApellido: this.formData.get('secondLastName')?.value,
      telefono: this.formData.get('phone')?.value,
      rfc: this.formData.get('rfc')?.value,
      calle: this.formAddres.get('stret')?.value,
      numero: this.formAddres.get('number')?.value,
      colonia: this.formAddres.get('city')?.value,
      codigoPostal: this.formAddres.get('postalCode')?.value
    };
    return model;
  }

}
