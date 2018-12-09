import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';

import { environment } from 'src/environments/environment';

@Injectable({
    providedIn: 'root'
})
export class AppService {

    constructor(private http: HttpClient) { }

    get<T>(ControllerName: any, ActionName: any) {
        let url = environment.apiUrl + '/' + ControllerName + '/' + ActionName;
        return this.http.get<any>(url).pipe(map((response: T) => response));
    }

    getById(ControllerName: any, ActionName: any, Id: any) {
        let url = environment.apiUrl + '/' + ControllerName + '/' + ActionName + '/' + Id;
        return this.http.get(url).pipe(map((response: any) => response));
    }

    post(ControllerName: any, ActionName: any, Data: any) {
        let url = environment.apiUrl + '/' + ControllerName + '/' + ActionName;
        return this.http.post(url, Data).pipe(map((response: any) => {
            return response != "" ? response : null;
        }));
    }
}
