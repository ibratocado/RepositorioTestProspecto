import { Component, OnInit } from '@angular/core';
import { ProspectoService } from 'src/app/services/prospecto.service';
import { StatusService } from 'src/app/services/status.service';

@Component({
  selector: 'app-page-add-prospecto',
  templateUrl: './page-add-prospecto.component.html',
  styleUrls: ['./page-add-prospecto.component.scss']
})
export class PageAddProspectoComponent implements OnInit {

  constructor(private statusService: StatusService,
    private prospectoService: ProspectoService) { }

  ngOnInit(): void {
    /*this.statusService.GetFull().then((data)=>{
      console.log("Status--->",data.respon);
    });

    this.prospectoService.GetFullByState(1).then((data)=>{
      console.log("Full--->",data.respon);
    });

    this.prospectoService.GetOneById("B6887851-3229-406B-B4BE-0AD59BD253F2").then((data)=>{
      console.log("One--->",data.respon);
    });*/

  }

}
