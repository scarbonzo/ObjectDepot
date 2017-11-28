import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import 'rxjs/add/operator/map';

@Injectable()
export class ObjectService {
  private _url = 'http://localhost:51242/api/v1/images/';

  constructor(private _http: Http) { }

  getObject(objectid: string) {
    return this._http.get(this._url + 'objectid')
    .map((_response: Response) => _response.json());
  }
}
