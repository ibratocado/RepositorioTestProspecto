import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  public active?: boolean;
  title = 'FrontProspecto';

  public handleChange(event: any){
      if( event.index== 1){
        this.active = true;
        return;
      }
      console.log(event.index);
      this.active = false;

  }
}
