import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {basicUrl} from "./basicUrl";

@Injectable({
  providedIn: 'root'
})
export class TestsApiService {

  private url: string = '/api/Test';

  constructor (private http: HttpClient) { }

  getTests(currentPageNumber, pageSize) {
    return this.http.get(basicUrl.apiUrl + this.url, {
      params: {
        pageNumber: currentPageNumber,
        pageSize: pageSize
      }});
  }
}
