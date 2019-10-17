import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import 'hammerjs';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MyMaterialModule} from './material/material.module';
import { LoginClientComponent } from './front/login-client/login-client.component';
import { ReactiveFormsModule } from '@angular/forms';
import { LayoutComponent } from './Template/layout/layout.component';
import {FlexLayoutModule} from '@angular/flex-layout';
import { HomeComponent } from './Template/home/home.component';
import { AccueilClientComponent } from './front/accueil-client/accueil-client.component';
import { SidenavListComponent } from './Template/sidenav-list/sidenav-list.component';
import { ListComptesComponent } from './front/list-comptes/list-comptes.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginClientComponent,
    LayoutComponent,
    HomeComponent,
    AccueilClientComponent,
    SidenavListComponent,
    ListComptesComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MyMaterialModule,
    ReactiveFormsModule,
    FlexLayoutModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})


export class AppModule {}