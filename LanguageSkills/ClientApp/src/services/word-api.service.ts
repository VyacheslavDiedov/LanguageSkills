import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {basicUrl} from "./basicUrl";

@Injectable({
  providedIn: 'root'
})
export class WordApiService {

  private url: string = '/api/Word';

  constructor (private http: HttpClient) { }

  getWord(subCategoryId) {
    return this.http.get(basicUrl.apiUrl + this.url + "/" + subCategoryId);
  }
}
