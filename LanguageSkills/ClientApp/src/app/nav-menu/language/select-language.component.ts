import { Component } from '@angular/core';

export class Language{
  id: number;
  languageName: string;
  imagePath: string;
}

@Component({
  selector: 'app-select-language-page',
  templateUrl: './select-language.component.html',
  styleUrls: ['./select-language.component.scss']
})
export class SelectLanguageComponent {


  public languages: Language[] = [
    {id: 1, languageName: 'English', imagePath: '../../../assets/img/Ukraine.jpg'},
    {id: 2, languageName: 'English', imagePath: '../../../assets/img/Ukraine.jpg'},
    {id: 3, languageName: 'English', imagePath: '../../../assets/img/Ukraine.jpg'},
    {id: 4, languageName: 'English', imagePath: '../../../assets/img/Ukraine.jpg'},
    {id: 5, languageName: 'English', imagePath: '../../../assets/img/Ukraine.jpg'},
    {id: 6, languageName: 'English', imagePath: '../../../assets/img/Ukraine.jpg'},
    {id: 7, languageName: 'English', imagePath: '../../../assets/img/Ukraine.jpg'},
    {id: 8, languageName: 'English', imagePath: '../../../assets/img/Ukraine.jpg'},
  ];
}
