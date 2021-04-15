import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {basicUrl} from "./basicUrl";

@Injectable({
  providedIn: 'root'
})
export class SubCategoryApiService {

  private url: string = '/api/SubCategory';

  constructor (private http: HttpClient) { }

  getSubCategory(categoryId, currentPageNumber, pageSize) {
    return this.http.get(basicUrl.apiUrl + this.url, {
      params: {
        categoryId: categoryId,
        pageNumber: currentPageNumber,
        pageSize: pageSize
      }});
  }
}
