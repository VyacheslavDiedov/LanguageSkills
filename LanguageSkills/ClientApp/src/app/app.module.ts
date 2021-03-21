import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import {SelectLanguageComponent} from './nav-menu/language/select-language.component';
import {CategoryComponent} from './nav-menu/language/category/category.component';
import {SubCategoryComponent} from './nav-menu/language/subCategory/subCategory.component';
import {SlideShowComponent} from './nav-menu/language/slideShow/slideShow.component';
import {BreadcrumbComponent} from '../common/breadcrumb/breadcrumb.component';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    SelectLanguageComponent,
    CategoryComponent,
    SubCategoryComponent,
    SlideShowComponent,
    BreadcrumbComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
    {
      path: '',
      component: SelectLanguageComponent,
      data:{
        breadcrumb: 'Select language'
      },
      children:[
        {
          path: 'category/:idLanguageToLearned',
          component: CategoryComponent,
          data: {
            breadcrumb: 'Category',
          }, children: [
            {
                  path: 'subCategory/:idCategory/:categoryName',
                  component: SubCategoryComponent,
                  data: {
                    breadcrumb: ''
                  },
              children: [
                {
                  path: 'slideShow/:idSubCategory/:subCategoryName',
                  component: SlideShowComponent,
                  data: {
                    breadcrumb: ''
                  },
                }
              ]
            }
          ]}
      ],
    },
    ]),
    //todo delete it after doing children
    // RouterModule.forRoot([
    //   {path: '', component: SelectLanguageComponent, pathMatch: 'full'},
    //   {path: 'category/:idLanguageToLearned', component: CategoryComponent },
    //   {path: 'subCategory/:idCategory/:categoryName', component: SubCategoryComponent },
    //   {path: 'slideShow/:idSubCategory/:subCategoryName', component: SlideShowComponent }
    // ])
],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
