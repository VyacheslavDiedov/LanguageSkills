import { Component, OnInit } from '@angular/core';
import { ActivatedRoute} from '@angular/router';


export class WordWithTranslation{
  id: number;
  wordImagePath: string;
  wordTranslationNativeName: string;
  wordTranslationLearnedName: string;
}

@Component({
  selector: 'app-slide-show-page',
  templateUrl: './slideShow.component.html',
  styleUrls: ['./slideShow.component.scss']
})
export class SlideShowComponent implements OnInit {
  subCategoryId: number;
  subCategoryName: string;
  currentWord: WordWithTranslation;

  constructor(private activateRoute: ActivatedRoute){
    this.subCategoryId = activateRoute.snapshot.params['idSubCategory']
    this.subCategoryName = activateRoute.snapshot.params['subCategoryName'];
  }

  ngOnInit() {
    this.wordWithTranslations.forEach((wordWithTranslation, i) => {
      setTimeout(() =>{
        this.currentWord = wordWithTranslation;
      }, i * 3000);
    })
  }

  nextSlide(){
    alert("next");
  }

  prevSlide(){
    alert("prev");
  }

  public wordWithTranslations: WordWithTranslation[] = [
    {id: 1, wordTranslationNativeName: 'Football', wordTranslationLearnedName: 'Футбол',
      wordImagePath: '../../../../assets/img/Ukraine.jpg'},
    {id: 2, wordTranslationNativeName: 'basketball', wordTranslationLearnedName: 'баскетбол',
      wordImagePath: '../../../../assets/img/Ukraine.jpg'},
    {id: 3, wordTranslationNativeName: 'volleyball', wordTranslationLearnedName: 'волейбол',
      wordImagePath: '../../../../assets/img/Ukraine.jpg'},
    {id: 4, wordTranslationNativeName: 'handball', wordTranslationLearnedName: 'гандбол',
      wordImagePath: '../../../../assets/img/Ukraine.jpg'},
    {id: 5, wordTranslationNativeName: 'rugby', wordTranslationLearnedName: 'регбі',
      wordImagePath: '../../../../assets/img/Ukraine.jpg'},
    {id: 6, wordTranslationNativeName: 'American football', wordTranslationLearnedName: 'американський футбол\n',
      wordImagePath: '../../../../assets/img/Ukraine.jpg'},
    {id: 7, wordTranslationNativeName: 'Football', wordTranslationLearnedName: 'Футбол',
      wordImagePath: '../../../../assets/img/Ukraine.jpg'},
    {id: 8, wordTranslationNativeName: 'Football', wordTranslationLearnedName: 'Футбол',
      wordImagePath: '../../../../assets/img/Ukraine.jpg'},
    {id: 9, wordTranslationNativeName: 'Football', wordTranslationLearnedName: 'Футбол',
      wordImagePath: '../../../../assets/img/Ukraine.jpg'},
    {id: 10, wordTranslationNativeName: 'Football', wordTranslationLearnedName: 'Футбол',
      wordImagePath: '../../../../assets/img/Ukraine.jpg'},
    {id: 11, wordTranslationNativeName: 'Football', wordTranslationLearnedName: 'Футбол',
      wordImagePath: '../../../../assets/img/Ukraine.jpg'},
    {id: 12, wordTranslationNativeName: 'Football', wordTranslationLearnedName: 'Футбол',
      wordImagePath: '../../../../assets/img/Ukraine.jpg'},
    {id: 13, wordTranslationNativeName: 'Football', wordTranslationLearnedName: 'Футбол',
      wordImagePath: '../../../../assets/img/Ukraine.jpg'},
    {id: 14, wordTranslationNativeName: 'Football', wordTranslationLearnedName: 'Футбол',
      wordImagePath: '../../../../assets/img/Ukraine.jpg'},
    {id: 15, wordTranslationNativeName: 'Football', wordTranslationLearnedName: 'Футбол',
      wordImagePath: '../../../../assets/img/Ukraine.jpg'},
  ];
}
