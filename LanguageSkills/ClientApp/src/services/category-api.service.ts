import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {basicUrl} from "./basicUrl";

@Injectable({
  providedIn: 'root'
})
export class CategoryApiService {

  private url: string = '/api/Category';

  constructor (private http: HttpClient) { }

  getCategory(languageToLearnId, nativeLanguageId, pageNumber, pageSize) {
    return this.http.get(basicUrl.apiUrl + this.url, {
      params: {
        languageToLearnId: languageToLearnId,
        nativeLanguageId: nativeLanguageId,
        pageNumber: pageNumber,
        pageSize: pageSize
      }});
  }
}
