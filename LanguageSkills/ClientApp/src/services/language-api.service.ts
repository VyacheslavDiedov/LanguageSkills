import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {basicUrl} from "./basicUrl";

@Injectable({
  providedIn: 'root'
})
export class LanguageApiService {

  private url: string = '/api/Language';

  constructor (private http: HttpClient) { }

  getLanguage() {
    return this.http.get(basicUrl.apiUrl + this.url);
  }
}
