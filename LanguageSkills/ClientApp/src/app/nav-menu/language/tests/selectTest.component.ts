import { Component, OnInit } from '@angular/core';
import { ActivatedRoute} from '@angular/router';
import 'hammerjs';
import 'hammer-timejs';
import {ItemWithTranslation} from '../../../../modules/ItemWithTranslation';
import {TestsApiService} from '../../../../services/tests-api.service';

@Component({
  selector: 'app-select-test-page',
  templateUrl: './selectTest.component.html',
  styleUrls: ['./selectTest.component.scss']
})
export class SelectTestComponent implements OnInit {
  testsData: Array<ItemWithTranslation>;
  subCategoryId: number;
  subCategoryName: string;
  innerWidth: number;
  isSuccessfullyCompleted: boolean;
  pageSize: number;
  currentPageNumber: number;
  totalPage: number;
  widthMobilePagination: number;

  constructor(private activateRoute: ActivatedRoute, public APIService: TestsApiService){
    this.subCategoryId = activateRoute.snapshot.params['idSubCategoryTest'];
    this.subCategoryName = activateRoute.snapshot.params['subCategoryNameTest'];
    this.innerWidth = window.innerWidth;
    this.isSuccessfullyCompleted = true;
    this.testsData = new Array<ItemWithTranslation>();
    this.currentPageNumber = 1;
    this.pageSize = this.innerWidth  > 650 ? 15 : 8;
    this.widthMobilePagination = 1;
  }

  ngOnInit() {
    this.loadData(this.currentPageNumber, this.pageSize);
  }

  loadData(currentPageNumber, pageSize){
    this.APIService.getTests(currentPageNumber, pageSize)
      .subscribe((data:{itemsWithTranslations, pageInfo}) => {
      this.testsData = data.itemsWithTranslations;
      this.currentPageNumber = data.pageInfo.pageNumber;
      this.totalPage = data.pageInfo.totalPages;
        this.widthMobilePagination = this.countSizeOfPagedLine(this.totalPage, this.currentPageNumber);
      });
  }

  countSizeOfPagedLine(totalPage, currentPage){
    return currentPage / totalPage * 100;
  }

  swipeRight(){
    if (this.currentPageNumber <= this.totalPage) {
      this.loadData(this.currentPageNumber + 1, this.pageSize);
    }
  }

  swipeLeft(){
    if (this.currentPageNumber > 1) {
      this.loadData(this.currentPageNumber - 1, this.pageSize);
    }
  }
}
