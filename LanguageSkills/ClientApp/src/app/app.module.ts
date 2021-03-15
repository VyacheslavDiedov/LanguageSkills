import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import {SelectLanguageComponent} from './nav-menu/language/select-language.component';
import {CategoryComponent} from './nav-menu/language/category/category.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    SelectLanguageComponent,
    CategoryComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      {path: '', component: SelectLanguageComponent, pathMatch: 'full'},
      {path: 'category/:idLanguageToLearned', component: CategoryComponent },
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
