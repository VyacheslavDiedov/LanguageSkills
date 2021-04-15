import { Component, OnInit } from '@angular/core';
import {ItemWithTranslation} from '../../../modules/ItemWithTranslation';
import {LanguageApiService} from '../../../services/language-api.service';

@Component({
  selector: 'app-select-language-page',
  templateUrl: './select-language.component.html',
  styleUrls: ['./select-language.component.scss']
})
export class SelectLanguageComponent implements OnInit{
  languageData: Array<ItemWithTranslation>;

  constructor(public APIService: LanguageApiService) {
    this.languageData = new Array<ItemWithTranslation>();
  }

  ngOnInit(): void {
    this.APIService.getLanguage().subscribe((data: ItemWithTranslation[]) => {
      this.languageData = data;
    });
  }
}
