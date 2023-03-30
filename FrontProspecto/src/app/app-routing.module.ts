import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DetailsProspectoComponent } from './components/details-prospecto/details-prospecto.component';
import { ListSendPospectoComponent } from './pages/list-send-pospecto/list-send-pospecto.component';
import { PageAddProspectoComponent } from './pages/page-add-prospecto/page-add-prospecto.component';

const routes: Routes = [
  {path:"AddProspect",component:DetailsProspectoComponent},
  {path:"ListProspect",component:ListSendPospectoComponent},
  {path:"",pathMatch: 'full',redirectTo:"AddProspect"}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
