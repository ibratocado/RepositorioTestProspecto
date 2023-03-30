import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { IdocumentRequest } from '../interfaces/idocument-request';
import { IdocumentRespon } from '../interfaces/idocument-respon';
import { IgenericRespon } from '../interfaces/igeneric-respon';
import { GenericService } from './generic.service';

@Injectable({
  providedIn: 'root'
})
export class DocumentsService {

  constructor(private baseService: GenericService) { }

  public getByProspecto(id: string): Promise<IgenericRespon<IdocumentRespon>>{
    let url = environment.url.document;
    return this.baseService.get(url,id);
  }

  public insertByProspecto(request: IdocumentRequest): Promise<IgenericRespon<any>>{
    let url = environment.url.document;
    let fromData = new FormData();

    fromData.append('ProspectoId',request.ProspectoId);
    return this.baseService.post(url,fromData);
  }
}
