import { Component, OnInit } from '@angular/core';
import { ActivatedRoute} from '@angular/router';
import 'hammerjs';
import 'hammer-timejs';


export class TestWithTranslation{
  id: number;
  testName: string;
  testTranslationNativeName: string;
  testTranslationLearnedName: string;
}
@Component({
  selector: 'app-select-test-page',
  templateUrl: './tests.component.html',
  styleUrls: ['./tests.component.scss']
})
export class TestsComponent implements OnInit {
  subCategoryId: number;
  subCategoryName: string;
  innerWidth: number;
  testWithTranslations: TestWithTranslation[];
  isSuccessfullyCompleted: boolean;

  constructor(private activateRoute: ActivatedRoute){
    this.subCategoryId = activateRoute.snapshot.params['idSubCategoryTest'];
    this.subCategoryName = activateRoute.snapshot.params['subCategoryNameTest'];
    this.innerWidth = window.innerWidth;
    this.isSuccessfullyCompleted = true;
  }

  ngOnInit() {
    //todo Make query
    if( this.innerWidth > 650){
      this.testWithTranslations = [
        {id: 1, testName: 'one from two', testTranslationNativeName: 'Traducción - pronunciación', testTranslationLearnedName: '正確\\錯誤' },
        {id: 2, testName: 'one from four', testTranslationNativeName: 'Traducción - pronunciación', testTranslationLearnedName: '正確\\錯誤' },
        {id: 3, testName: 'one from four: text', testTranslationNativeName: 'Traducción - pronunciación', testTranslationLearnedName: '正確\\錯誤' },
        {id: 4, testName: 'one from four: listening', testTranslationNativeName: 'Traducción - pronunciación', testTranslationLearnedName: '正確\\錯誤' },
        {id: 5, testName: 'True\\False', testTranslationNativeName: 'Traducción - pronunciación', testTranslationLearnedName: '正確\\錯誤' },
        {id: 6, testName: 'Spell with picture', testTranslationNativeName: 'Traducción - pronunciación', testTranslationLearnedName: '正確\\錯誤' },
        {id: 7, testName: 'Spell with voice', testTranslationNativeName: 'Traducción - pronunciación', testTranslationLearnedName: '正確\\錯誤' },
        {id: 8, testName: 'Translation - text', testTranslationNativeName: 'Traducción - pronunciación', testTranslationLearnedName: '正確\\錯誤' },
        {id: 9, testName: 'Translation - pronunciation', testTranslationNativeName: 'Traducción - pronunciación', testTranslationLearnedName: '正確\\錯誤' },
        {id: 10, testName: 'Find pair - translation', testTranslationNativeName: 'Traducción - pronunciación', testTranslationLearnedName: '正確\\錯誤' },
      ];
    }else {
      this.testWithTranslations = [
        {id: 1, testName: 'one from two', testTranslationNativeName: 'Traducción - pronunciación', testTranslationLearnedName: '正確\\錯誤' },
        {id: 2, testName: 'one from four', testTranslationNativeName: 'Traducción - pronunciación', testTranslationLearnedName: '正確\\錯誤' },
        {id: 3, testName: 'one from four: text', testTranslationNativeName: 'Traducción - pronunciación', testTranslationLearnedName: '正確\\錯誤' },
        {id: 4, testName: 'one from four: listening', testTranslationNativeName: 'Traducción - pronunciación', testTranslationLearnedName: '正確\\錯誤' },
        {id: 5, testName: 'True\\False', testTranslationNativeName: 'Traducción - pronunciación', testTranslationLearnedName: '正確\\錯誤' },
        {id: 6, testName: 'Spell with picture', testTranslationNativeName: 'Traducción - pronunciación', testTranslationLearnedName: '正確\\錯誤' },
        {id: 7, testName: 'Translation - text', testTranslationNativeName: 'Traducción - pronunciación', testTranslationLearnedName: '正確\\錯誤' },
        {id: 8, testName: 'Translation - pronunciation', testTranslationNativeName: 'Traducción - pronunciación', testTranslationLearnedName: '正確\\錯誤' },
      ];
    }
  }

  swipeRight(){
    if( this.innerWidth < 650){
      alert("right")
    }
  }

  swipeLeft(){
    if( this.innerWidth < 650) {
      alert("left")
    }
  }
}
