import {Component, ElementRef, OnInit, ViewChild} from '@angular/core';
import { ActivatedRoute} from '@angular/router';
import 'hammerjs';
import 'hammer-timejs';


export class TestWithTranslation{
  id: number;
  testName: string;
  testTranslationNativeName: string;
  testTranslationLearnedName: string;
  isCorrect: boolean;
}

@Component({
  selector: 'app-select-test-page',
  templateUrl: './test.component.html',
  styleUrls: ['./test.component.scss']
})

export class TestComponent implements OnInit {
  subCategoryId: number;
  subCategoryName: string;
  testId: number;
  testName: string;
  innerWidth: number;
  testWithTranslations: TestWithTranslation[];
  isSuccessfullyCompleted: boolean;
  canvasWidth: number;

  @ViewChild('myCanvas', {static: false}) myCanvas: ElementRef;

  constructor(private activateRoute: ActivatedRoute){
    this.subCategoryId = activateRoute.snapshot.params['idSubCategoryTest'];
    this.subCategoryName = activateRoute.snapshot.params['subCategoryNameTest'];
    this.testId = activateRoute.snapshot.params['idTest'];
    this.testName = activateRoute.snapshot.params['testName'];
    this.innerWidth = window.innerWidth;
    this.isSuccessfullyCompleted = true;
  }

  ngOnInit() {
      if(this.innerWidth > 650){
        this.canvasWidth = 261;
      }else {
        this.canvasWidth =77;
      }
    //todo Make query
      this.testWithTranslations = [
        {id: 1, testName: 'one from two', testTranslationNativeName: 'Traducción - pronunciación', testTranslationLearnedName: '正確\\錯誤', isCorrect: true},
        {id: 2, testName: 'one from four', testTranslationNativeName: 'Traducción - pronunciación', testTranslationLearnedName: '正確\\錯誤', isCorrect: true},
        {id: 3, testName: 'one from four: text', testTranslationNativeName: 'Traducción - pronunciación', testTranslationLearnedName: '正確\\錯誤', isCorrect: true },
        {id: 4, testName: 'one from four: listening', testTranslationNativeName: 'Traducción - pronunciación', testTranslationLearnedName: '正確\\錯誤', isCorrect: false },
        {id: 5, testName: 'True\\False', testTranslationNativeName: 'Traducción - pronunciación', testTranslationLearnedName: '正確\\錯誤', isCorrect: null },
        {id: 6, testName: 'Spell with picture', testTranslationNativeName: 'Traducción - pronunciación', testTranslationLearnedName: '正確\\錯誤', isCorrect: null },
        {id: 7, testName: 'Spell with voice', testTranslationNativeName: 'Traducción - pronunciación', testTranslationLearnedName: '正確\\錯誤', isCorrect: null },
        {id: 8, testName: 'Translation - text', testTranslationNativeName: 'Traducción - pronunciación', testTranslationLearnedName: '正確\\錯誤', isCorrect: null },
        {id: 9, testName: 'Translation - pronunciation', testTranslationNativeName: 'Traducción - pronunciación', testTranslationLearnedName: '正確\\錯誤', isCorrect: null },
        {id: 10, testName: 'Find pair - translation', testTranslationNativeName: 'Traducción - pronunciación', testTranslationLearnedName: '正確\\錯誤', isCorrect: null },
        {id: 11, testName: 'one from two', testTranslationNativeName: 'Traducción - pronunciación', testTranslationLearnedName: '正確\\錯誤', isCorrect: true},
        {id: 12, testName: 'one from four', testTranslationNativeName: 'Traducción - pronunciación', testTranslationLearnedName: '正確\\錯誤', isCorrect: true},
        {id: 13, testName: 'one from four: text', testTranslationNativeName: 'Traducción - pronunciación', testTranslationLearnedName: '正確\\錯誤', isCorrect: true },
        {id: 14, testName: 'one from four: listening', testTranslationNativeName: 'Traducción - pronunciación', testTranslationLearnedName: '正確\\錯誤', isCorrect: false },
        {id: 15, testName: 'True\\False', testTranslationNativeName: 'Traducción - pronunciación', testTranslationLearnedName: '正確\\錯誤', isCorrect: null },
        {id: 16, testName: 'Spell with picture', testTranslationNativeName: 'Traducción - pronunciación', testTranslationLearnedName: '正確\\錯誤', isCorrect: null },
        {id: 17, testName: 'Spell with voice', testTranslationNativeName: 'Traducción - pronunciación', testTranslationLearnedName: '正確\\錯誤', isCorrect: null },
        {id: 18, testName: 'Translation - text', testTranslationNativeName: 'Traducción - pronunciación', testTranslationLearnedName: '正確\\錯誤', isCorrect: null },
        {id: 19, testName: 'Translation - pronunciation', testTranslationNativeName: 'Traducción - pronunciación', testTranslationLearnedName: '正確\\錯誤', isCorrect: null },
        {id: 20, testName: 'Find pair - translation', testTranslationNativeName: 'Traducción - pronunciación', testTranslationLearnedName: '正確\\錯誤', isCorrect: null },
        {id: 20, testName: 'Find pair - translation', testTranslationNativeName: 'Traducción - pronunciación', testTranslationLearnedName: '正確\\錯誤', isCorrect: null },
      ];
  }

  drawLine(connectTo = '1vs1', isCorrect = true) {
    let canvas = document.getElementById("myCanvas") as HTMLCanvasElement,
      context = canvas.getContext("2d");

    if(isCorrect){
      context.strokeStyle = "#00CC83";
    } else {
      context.strokeStyle = "#EE4444";
    }

    context.lineWidth   = 2;
    context.beginPath();

    if(this.innerWidth > 650){

      switch (connectTo) {

        case '1vs1':
          context.moveTo(-500, 25);
          context.lineTo(500, 25);
            break;
        case '1vs2':
          context.moveTo(0, 25);
          context.bezierCurveTo(250, -30, 20, 200, 350, 70);
            break;
        case '1vs3':
          context.moveTo(0, 25);
          context.bezierCurveTo(250, -30, 0, 300, 350, 140);
            break;
        case '1vs4':
          context.moveTo(0, 25);
          context.bezierCurveTo(250, -30, -20, 400, 350, 220);
            break;
        case '2vs1':
          context.moveTo(350, 90);
          context.bezierCurveTo(-70, 180, 400, -70, -200, 70);
            break;
        case '3vs1':
          context.moveTo(350, 160);
          context.bezierCurveTo(-70, 250, 400, -90, -200, 70);
            break;
        case '4vs1':
          context.moveTo(350, 250);
          context.bezierCurveTo(-90, 350, 400, -90, -200, 70);
            break
      }
    }else {

      switch (connectTo) {
        case '1vs1':
          context.moveTo(-500, 25);
          context.lineTo(500, 25);
            break;
        case '1vs2':
          context.moveTo(0, 25);
          context.bezierCurveTo(100, 10, -50, 150, 150, 75);
            break;
        case '1vs3':
          context.moveTo(0, 25);
          context.bezierCurveTo(100, 10, -80, 300, 250, 70);
            break;
        case '1vs4':
          context.moveTo(0, 25);
          context.bezierCurveTo(100, 10, -80, 450, 250, 70);
            break;
        case '2vs1':
          context.moveTo(270, 110);
          context.bezierCurveTo(-90, -90, 110, 120, 0, 90);
            break;
        case '3vs1':
          context.moveTo(250, 90);
          context.bezierCurveTo(-90, -90, 110, 150, -0, 150);
            break;
        case '4vs1':
          context.moveTo(220, 70);
          context.bezierCurveTo(-90, -90, 110, 230, -0, 220);
            break
      }
    }

    context.stroke();
  }
}
