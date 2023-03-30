import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { IgenericRespon } from '../interfaces/igeneric-respon';
import { IstatusRespon } from '../interfaces/istatus-respon';
import { GenericService } from './generic.service';

@Injectable({
  providedIn: 'root'
})
export class StatusService {

  constructor(private baseService: GenericService) { }

  public GetFull(): Promise<IgenericRespon<IstatusRespon>>{
    var url = environment.url.status.getFull;
    return this.baseService.get(url);
  }
}
