import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import 'hammerjs';
import 'hammer-timejs';
import {ItemWithTranslation} from '../../../../modules/ItemWithTranslation';
import {CategoryApiService} from '../../../../services/category-api.service';

@Component({
  selector: 'app-select-category-page',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.scss']
})

export class CategoryComponent implements OnInit{
  categoryData: Array<ItemWithTranslation>;
  pageInfo: {};
  pagedPageNames: Array<number>;
  nativeLanguageId: number;
  idLanguageToLearned: number;
  currentPageNumber: number;
  pageSize: number;
  totalPage: number;
  innerWidth: number;
  widthPagination: number;

  constructor(private activateRoute: ActivatedRoute, public APIService: CategoryApiService){
    this.categoryData = new Array<ItemWithTranslation>();
    this.pageInfo = new Object();
    this.pagedPageNames = new Array<number>();
    this.idLanguageToLearned = activateRoute.snapshot.params['idLanguageToLearned'];
    this.pageSize = window.innerWidth > 650 ? 15 : 8;
    this.currentPageNumber = 1;

    //todo It will change after doing authorization
    this.nativeLanguageId = 3;
    this.innerWidth = window.innerWidth;
    this.widthPagination = 1;
  }

  ngOnInit() {
    this.loadData(this.idLanguageToLearned, this.nativeLanguageId, this.currentPageNumber, this.pageSize);
  }

  loadData(idLanguageToLearned, nativeLanguageId, pageNumber, pageSize){
    this.APIService.getCategory(idLanguageToLearned, nativeLanguageId, pageNumber, pageSize)
      .subscribe((data:{itemsWithTranslations, pageInfo}) => {
        this.categoryData = data.itemsWithTranslations;
        this.pageInfo = data.pageInfo;
        this.currentPageNumber = data.pageInfo.pageNumber;
        this.totalPage = data.pageInfo.totalPages;
        this.makeArrayForPagination(this.pageInfo);
        this.widthPagination = this.countSizeOfPagedLine(this.totalPage, this.currentPageNumber);
      });
  }

  makeArrayForPagination(totalPage){
    for (let i = 1; i <= totalPage.totalPages; i++){
      this.pagedPageNames[i - 1] = i;
    }
  }

  countSizeOfPagedLine(totalPage, currentPage){
    return currentPage / totalPage * 100;
  }

  isActivePage(pageNumber) {
    return pageNumber === this.currentPageNumber ? true : false;
  }

  pageSelection(pageNumber) {
    if(pageNumber !== this.currentPageNumber) {
      this.loadData(this.idLanguageToLearned, this.nativeLanguageId, pageNumber, this.pageSize);
    }
  }

  prevPage(pageNumber) {
    if (pageNumber > 1) {
      this.loadData(this.idLanguageToLearned, this.nativeLanguageId, pageNumber - 1, this.pageSize);
    }
  }

  nextPage(pageNumber) {
    if (pageNumber < this.totalPage) {
      this.loadData(this.idLanguageToLearned, this.nativeLanguageId, pageNumber + 1, this.pageSize);
    }
  }

  swipeRight(){
      if (this.currentPageNumber <= this.totalPage) {
        this.loadData(this.idLanguageToLearned, this.nativeLanguageId, this.currentPageNumber + 1, this.pageSize);
      }
  }

  swipeLeft(){
      if (this.currentPageNumber > 1) {
        this.loadData(this.idLanguageToLearned, this.nativeLanguageId, this.currentPageNumber - 1, this.pageSize);
      }
  }
}
