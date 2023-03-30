import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { IgenericRespon } from '../interfaces/igeneric-respon';
import { IprospectoRequest } from '../interfaces/iprospecto-request';
import { IprospectoRespon } from '../interfaces/iprospecto-respon';
import { IprospectoStatusRequest } from '../interfaces/iprospecto-status-request';
import { GenericService } from './generic.service';

@Injectable({
  providedIn: 'root'
})
export class ProspectoService {

  constructor(private baseService: GenericService) { }

  public GetFullByState(state: number): Promise<IgenericRespon<IprospectoRespon>>{
    var url = environment.url.prospecto.getFullForStatus;
    return this.baseService.get(url,state);
  }

  public GetOneById(id: string): Promise<IgenericRespon<IprospectoRespon>>{
    var url = environment.url.prospecto.getOneById;
    return this.baseService.get(url,id);
  }

  public Post(model: IprospectoRequest): Promise<IgenericRespon<string>>{
    var url = environment.url.prospecto.post;
    return this.baseService.post(url,model);
  }

  public putStatus(model: IprospectoStatusRequest): Promise<IgenericRespon<any>>{
    var url = environment.url.prospecto.putStatus;
    return this.baseService.put(url,model);
  }
}
