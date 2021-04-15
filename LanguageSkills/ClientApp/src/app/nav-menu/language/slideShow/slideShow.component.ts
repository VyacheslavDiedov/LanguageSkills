import { Component, OnInit, DoCheck } from '@angular/core';
import { ActivatedRoute} from '@angular/router';
import {ItemWithTranslation} from '../../../../modules/ItemWithTranslation';
import {WordApiService} from '../../../../services/word-api.service';

@Component({
  selector: 'app-slide-show-page',
  templateUrl: './slideShow.component.html',
  styleUrls: ['./slideShow.component.scss']
})

export class SlideShowComponent implements OnInit, DoCheck {
  wordData: Array<ItemWithTranslation>;
  subCategoryId: number;
  subCategoryName: string;
  currentWord: ItemWithTranslation;
  currentIterator: number;
  isAutomaticallySlideShowStarted: boolean;
  isTest: boolean;

  constructor(private activateRoute: ActivatedRoute, public APIService: WordApiService){
    this.subCategoryId = activateRoute.snapshot.params['idSubCategory'];
    this.subCategoryName = activateRoute.snapshot.params['subCategoryName'];
    this.wordData = new Array<ItemWithTranslation>();
    this.currentIterator = 0;
    this.isAutomaticallySlideShowStarted = false;
    this.isTest = false;
  }

  ngOnInit() {
    this.loadData(this.subCategoryId);
  }

  loadData(subCategoryId){
    this.APIService.getWord(subCategoryId)
      .subscribe((data: ItemWithTranslation[]) => {
        this.wordData = data;
        this.slideShow(this.currentIterator);
      });
  }

  slideShow(i){
    if(i < this.wordData.length && i >= 0){
      this.currentWord = this.wordData[i];
    }
  }

  automaticallySlideShow(){
    if(!this.isAutomaticallySlideShowStarted){
      this.isAutomaticallySlideShowStarted = true;
      this.wordData.forEach((wordWithTranslation, i) => {
        setTimeout(() =>{
          i = this.currentIterator;
          this.slideShow(this.currentIterator);
          this.incrementIterator();
        }, i * 3000);
      })
    }else {
      this.nextSlide();
    }

  }

  nextSlide(){
    this.incrementIterator();
    this.slideShow(this.currentIterator);
  }

  prevSlide(){
    this.decrementIterator()
    this.slideShow(this.currentIterator);
  }

  incrementIterator(){
    if(this.currentIterator < this.wordData.length - 1){
      this.currentIterator++;
    }
  }

  decrementIterator(){
    if(this.currentIterator >= 0){
      this.currentIterator--;
    }
  }

  resetSlideShow(){
    this.currentIterator = 0;
    this.slideShow(this.currentIterator);
  }

  ngDoCheck() {
    if (this.wordData.length != 0 && this.currentIterator > this.wordData.length - 5) {
      this.isTest = true
    }
  }
}
