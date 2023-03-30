import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

//PrimeNg
import {ButtonModule} from 'primeng/button';
import {InputTextModule} from 'primeng/inputtext';
import {InputMaskModule} from 'primeng/inputmask';
import {InputNumberModule} from 'primeng/inputnumber';
import {CardModule} from 'primeng/card';
import {SplitButtonModule} from 'primeng/splitbutton';
import {FileUploadModule} from 'primeng/fileupload';
import {ToggleButtonModule} from 'primeng/togglebutton';
import {ToastModule} from 'primeng/toast';

import { PageAddProspectoComponent } from './pages/page-add-prospecto/page-add-prospecto.component';
import { HttpClientModule } from '@angular/common/http';
import { FormProspectoComponent } from './components/form-prospecto/form-prospecto.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MessageService } from 'primeng/api';

@NgModule({
  declarations: [
    AppComponent,
    PageAddProspectoComponent,
    FormProspectoComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    ButtonModule,
    HttpClientModule,
    InputTextModule,
    InputMaskModule,
    InputNumberModule,
    CardModule,
    ReactiveFormsModule,
    SplitButtonModule,
    FileUploadModule,
    ToggleButtonModule,
    FormsModule,
    ToastModule
  ],
  providers: [MessageService],
  bootstrap: [AppComponent]
})
export class AppModule { }
