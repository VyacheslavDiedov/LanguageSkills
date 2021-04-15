import { Component, OnInit } from '@angular/core';
import { ActivatedRoute} from '@angular/router';
import 'hammerjs';
import 'hammer-timejs';
import {ItemWithTranslation} from '../../../../modules/ItemWithTranslation';
import {SubCategoryApiService} from '../../../../services/subCategory-api.service';

@Component({
  selector: 'app-select-subCategory-page',
  templateUrl: './subCategory.component.html',
  styleUrls: ['./subCategory.component.scss']
})
export class SubCategoryComponent implements OnInit {
  subCategoryData: Array<ItemWithTranslation>;
  pageInfo: {};
  pagedPageNames: Array<number>;
  pageSize: number;
  totalPage: number;
  currentPageNumber: number;
  categoryId: number;
  categoryName: string;
  innerWidth: number;
  widthMobilePagination: number;

  constructor(private activateRoute: ActivatedRoute, public APIService: SubCategoryApiService) {
    this.subCategoryData = new Array<ItemWithTranslation>();
    this.pageInfo = new Object();
    this.pagedPageNames = new Array<number>();
    this.categoryId = activateRoute.snapshot.params['idCategory'];
    this.categoryName = activateRoute.snapshot.params['categoryName'];
    this.currentPageNumber = 1;
    this.innerWidth = window.innerWidth;
    this.widthMobilePagination = 1;
    this.pageSize = this.innerWidth  > 650 ? 15 : 8;
  }

  ngOnInit() {
    this.loadData(this.categoryId, this.currentPageNumber, this.pageSize);
  }

  loadData(categoryId, pageNumber, pageSize){
    this.APIService.getSubCategory(categoryId, pageNumber, pageSize)
      .subscribe((data:{itemsWithTranslations, pageInfo}) => {
        this.subCategoryData = data.itemsWithTranslations;
        this.pageInfo = data.pageInfo;
        this.currentPageNumber = data.pageInfo.pageNumber;
        this.totalPage = data.pageInfo.totalPages;
        this.makeArrayForPagination(this.pageInfo);
        this.widthMobilePagination = this.countSizeOfPagedLine(this.totalPage, this.currentPageNumber);
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
    if(pageNumber !== this.currentPageNumber){
      this.loadData(this.categoryId, pageNumber, this.pageSize);
    }
  }

  prevPage(pageNumber) {
    if (pageNumber > 1) {
      this.loadData(this.categoryId, pageNumber - 1, this.pageSize);
    }
  }

  nextPage(pageNumber) {
    if (pageNumber < this.totalPage) {
      this.loadData(this.categoryId, pageNumber + 1, this.pageSize);
    }
  }

  swipeRight(){
    if (this.currentPageNumber <= this.totalPage) {
      this.loadData(this.categoryId, this.currentPageNumber + 1, this.pageSize);
    }
  }

  swipeLeft(){
    if (this.currentPageNumber > 1) {
      this.loadData(this.categoryId, this.currentPageNumber - 1, this.pageSize);
    }
  }
}

