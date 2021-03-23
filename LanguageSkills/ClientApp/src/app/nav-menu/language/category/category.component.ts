import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from '@angular/router';

export class CategoryWithTranslation{
  id: number;
  categoryName: string;
  categoryImagePath: string;
  categoryTranslationLearnedName: string;
}

@Component({
  selector: 'app-select-category-page',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.scss']
})
export class CategoryComponent implements OnInit{
  idLanguageToLearned: number;
  innerWidth: number;
  categoryWithTranslations: CategoryWithTranslation[];

  constructor(private activateRoute: ActivatedRoute){
    this.idLanguageToLearned = activateRoute.snapshot.params['idLanguageToLearned'];
    this.innerWidth = window.innerWidth;
  }

  ngOnInit() {
    //todo it will get from back end
    if( this.innerWidth > 650){
    this.categoryWithTranslations = [
        {id: 1, categoryName: 'Nature and environment', categoryImagePath: '../../../../assets/img/Ukraine.jpg', categoryTranslationLearnedName: 'Nature and environment'},
        {id: 2, categoryName: 'Animal', categoryImagePath: '../../../../assets/img/Ukraine.jpg', categoryTranslationLearnedName: 'Animal'},
        {id: 3, categoryName: 'Animal', categoryImagePath: '../../../../assets/img/Ukraine.jpg', categoryTranslationLearnedName: 'Animal'},
        {id: 4, categoryName: 'Animal', categoryImagePath: '../../../../assets/img/Ukraine.jpg', categoryTranslationLearnedName: 'Animal'},
        {id: 5, categoryName: 'Animal', categoryImagePath: '../../../../assets/img/Ukraine.jpg', categoryTranslationLearnedName: 'Animal'},
        {id: 6, categoryName: 'Animal', categoryImagePath: '../../../../assets/img/Ukraine.jpg', categoryTranslationLearnedName: 'Animal'},
        {id: 7, categoryName: 'Animal', categoryImagePath: '../../../../assets/img/Ukraine.jpg', categoryTranslationLearnedName: 'Animal'},
        {id: 8, categoryName: 'Animal', categoryImagePath: '../../../../assets/img/Ukraine.jpg', categoryTranslationLearnedName: 'Animal'},
        {id: 9, categoryName: 'Animal', categoryImagePath: '../../../../assets/img/Ukraine.jpg', categoryTranslationLearnedName: 'Animal'},
        {id: 10, categoryName: 'Animal', categoryImagePath: '../../../../assets/img/Ukraine.jpg', categoryTranslationLearnedName: 'Animal'},
        {id: 11, categoryName: 'Animal', categoryImagePath: '../../../../assets/img/Ukraine.jpg', categoryTranslationLearnedName: 'Animal'},
        {id: 12, categoryName: 'Animal', categoryImagePath: '../../../../assets/img/Ukraine.jpg', categoryTranslationLearnedName: 'Animal'},
        {id: 13, categoryName: 'Animal', categoryImagePath: '../../../../assets/img/Ukraine.jpg', categoryTranslationLearnedName: 'Animal'},
        {id: 14, categoryName: 'Animal', categoryImagePath: '../../../../assets/img/Ukraine.jpg', categoryTranslationLearnedName: 'Animal'},
        {id: 15, categoryName: 'Animal', categoryImagePath: '../../../../assets/img/Ukraine.jpg', categoryTranslationLearnedName: 'Animal'},
      ];
    }else{
      this.categoryWithTranslations = [
        {id: 1, categoryName: 'Nature and environment', categoryImagePath: '../../../../assets/img/Ukraine.jpg', categoryTranslationLearnedName: 'Nature and environment'},
        {id: 2, categoryName: 'Animal', categoryImagePath: '../../../../assets/img/Ukraine.jpg', categoryTranslationLearnedName: 'Animal'},
        {id: 3, categoryName: 'Animal', categoryImagePath: '../../../../assets/img/Ukraine.jpg', categoryTranslationLearnedName: 'Animal'},
        {id: 4, categoryName: 'Animal', categoryImagePath: '../../../../assets/img/Ukraine.jpg', categoryTranslationLearnedName: 'Animal'},
        {id: 5, categoryName: 'Animal', categoryImagePath: '../../../../assets/img/Ukraine.jpg', categoryTranslationLearnedName: 'Animal'},
        {id: 6, categoryName: 'Animal', categoryImagePath: '../../../../assets/img/Ukraine.jpg', categoryTranslationLearnedName: 'Animal'},
        {id: 7, categoryName: 'Animal', categoryImagePath: '../../../../assets/img/Ukraine.jpg', categoryTranslationLearnedName: 'Animal'},
        {id: 8, categoryName: 'Animal', categoryImagePath: '../../../../assets/img/Ukraine.jpg', categoryTranslationLearnedName: 'Animal'},
      ];
    }
  }



  isActive(pageNumber) {
    if (pageNumber === 2) {
      return true;
    } else {
      return false;
    }
  }

  pageSelection(pageNumber) {
    alert(pageNumber);
  }

  prevPage(pageNumber) {
    if (pageNumber > 1) {
      alert(pageNumber - 1);
    }
  }

  nextPage(pageNumber) {
    if (pageNumber < 4) {
      alert(pageNumber + 1);
    }
  }
}
