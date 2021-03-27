import { Component, OnInit } from '@angular/core';
import { ActivatedRoute} from '@angular/router';
import 'hammerjs';
import 'hammer-timejs';


export class SubCategoryWithTranslation{
  id: number;
  subCategoryName: string;
  subCategoryImagePath: string;
  subCategoryTranslationLearnedName: string;
}

@Component({
  selector: 'app-select-subCategory-page',
  templateUrl: './subCategory.component.html',
  styleUrls: ['./subCategory.component.scss']
})
export class SubCategoryComponent implements OnInit {
  categoryId: number;
  categoryName: string;
  innerWidth: number;
  subCategoryWithTranslations: SubCategoryWithTranslation[];
  widthValue: number;

  constructor(private activateRoute: ActivatedRoute){
    this.categoryId = activateRoute.snapshot.params['idCategory'];
    this.categoryName = activateRoute.snapshot.params['categoryName'];
    this.innerWidth = window.innerWidth;
    this.widthValue = 79;
  }

  ngOnInit() {
    //todo Make query
    if( this.innerWidth > 650){
      this.subCategoryWithTranslations = [
        {id: 1, subCategoryName: 'Kinds of sport 1', subCategoryImagePath: '../../../../assets/img/Ukraine.jpg', subCategoryTranslationLearnedName: 'Kinds of sport 1'},
        {id: 2, subCategoryName: 'Kinds of sport 1', subCategoryImagePath: '../../../../assets/img/Ukraine.jpg', subCategoryTranslationLearnedName: 'Kinds of sport 1'},
        {id: 3, subCategoryName: 'Kinds of sport 1', subCategoryImagePath: '../../../../assets/img/Ukraine.jpg', subCategoryTranslationLearnedName: 'Kinds of sport 1'},
        {id: 4, subCategoryName: 'Kinds of sport 1', subCategoryImagePath: '../../../../assets/img/Ukraine.jpg', subCategoryTranslationLearnedName: 'Kinds of sport 1'},
        {id: 5, subCategoryName: 'Kinds of sport 1', subCategoryImagePath: '../../../../assets/img/Ukraine.jpg', subCategoryTranslationLearnedName: 'Kinds of sport 1'},
        {id: 6, subCategoryName: 'Kinds of sport 1', subCategoryImagePath: '../../../../assets/img/Ukraine.jpg', subCategoryTranslationLearnedName: 'Kinds of sport 1'},
        {id: 7, subCategoryName: 'Kinds of sport 1', subCategoryImagePath: '../../../../assets/img/Ukraine.jpg', subCategoryTranslationLearnedName: 'Kinds of sport 1'},
        {id: 8, subCategoryName: 'Kinds of sport 1', subCategoryImagePath: '../../../../assets/img/Ukraine.jpg', subCategoryTranslationLearnedName: 'Kinds of sport 1'},
        {id: 9, subCategoryName: 'Kinds of sport 1', subCategoryImagePath: '../../../../assets/img/Ukraine.jpg', subCategoryTranslationLearnedName: 'Kinds of sport 1'},
        {id: 10, subCategoryName: 'Kinds of sport 1', subCategoryImagePath: '../../../../assets/img/Ukraine.jpg', subCategoryTranslationLearnedName: 'Kinds of sport 1'},
        {id: 11, subCategoryName: 'Kinds of sport 1', subCategoryImagePath: '../../../../assets/img/Ukraine.jpg', subCategoryTranslationLearnedName: 'Kinds of sport 1'},
        {id: 12, subCategoryName: 'Kinds of sport 1', subCategoryImagePath: '../../../../assets/img/Ukraine.jpg', subCategoryTranslationLearnedName: 'Kinds of sport 1'},
        {id: 13, subCategoryName: 'Kinds of sport 1', subCategoryImagePath: '../../../../assets/img/Ukraine.jpg', subCategoryTranslationLearnedName: 'Kinds of sport 1'},
        {id: 14, subCategoryName: 'Kinds of sport 1', subCategoryImagePath: '../../../../assets/img/Ukraine.jpg', subCategoryTranslationLearnedName: 'Kinds of sport 1'},
        {id: 15, subCategoryName: 'Kinds of sport 1', subCategoryImagePath: '../../../../assets/img/Ukraine.jpg', subCategoryTranslationLearnedName: 'Kinds of sport 1'},
      ];
    }else {
      this.subCategoryWithTranslations = [
        {id: 1, subCategoryName: 'Kinds of sport 1', subCategoryImagePath: '../../../../assets/img/Ukraine.jpg', subCategoryTranslationLearnedName: 'Kinds of sport 1'},
        {id: 2, subCategoryName: 'Kinds of sport 1', subCategoryImagePath: '../../../../assets/img/Ukraine.jpg', subCategoryTranslationLearnedName: 'Kinds of sport 1'},
        {id: 3, subCategoryName: 'Kinds of sport 1', subCategoryImagePath: '../../../../assets/img/Ukraine.jpg', subCategoryTranslationLearnedName: 'Kinds of sport 1'},
        {id: 4, subCategoryName: 'Kinds of sport 1', subCategoryImagePath: '../../../../assets/img/Ukraine.jpg', subCategoryTranslationLearnedName: 'Kinds of sport 1'},
        {id: 5, subCategoryName: 'Kinds of sport 1', subCategoryImagePath: '../../../../assets/img/Ukraine.jpg', subCategoryTranslationLearnedName: 'Kinds of sport 1'},
        {id: 6, subCategoryName: 'Kinds of sport 1', subCategoryImagePath: '../../../../assets/img/Ukraine.jpg', subCategoryTranslationLearnedName: 'Kinds of sport 1'},
        {id: 7, subCategoryName: 'Kinds of sport 1', subCategoryImagePath: '../../../../assets/img/Ukraine.jpg', subCategoryTranslationLearnedName: 'Kinds of sport 1'},
        {id: 8, subCategoryName: 'Kinds of sport 1', subCategoryImagePath: '../../../../assets/img/Ukraine.jpg', subCategoryTranslationLearnedName: 'Kinds of sport 1'},
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
