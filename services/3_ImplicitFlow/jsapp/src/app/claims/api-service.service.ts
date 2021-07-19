import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

export interface Claim {
  type: string;
  value: string;
}


@Injectable({
  providedIn: 'root'
})
export class ApiServiceService {

  private readonly url = '/identity';

  constructor(private http:HttpClient) { }

  getClaims():Observable<Array<Claim>>{
    return this.http.get<Array<Claim>>(this.url);
  }

}
