import { HttpClient, HttpHeaders, HttpResponse, HttpResponseBase } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { FilterModel } from '../models/filter.model';

@Injectable({
  providedIn: 'root'
})
export class HttpService {

  constructor(private http: HttpClient) { }

  public get<T>(url: string): Observable<HttpResponse<T>> {
    return this.http.get<HttpResponse<T>>(url, this.getRequestHeaders());
  }

  public getPaginationApiUrl(filter: FilterModel): string {
    const skip = filter.skip || 0;
    const limit = filter.limit || 20;
    let url = `${environment.apiUrl}/api/v1.0/products?limit=${limit}`;

    if (skip > 0) {
      url = `${url}&skip=${skip}`;
    }

    return url;
  }

  private getRequestHeaders(): {
    headers: HttpHeaders | { [header: string]: string | string[]; }
  } {
    let headers = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Accept': 'application/json'
      }),
      observe: 'response'
    };

    return headers;
  }

}
