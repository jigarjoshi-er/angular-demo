import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { map } from 'rxjs/operators';

import { CookiesService } from '../services/cookies.service';
import { environment } from '../../environments/environment';

@Injectable({
	providedIn: 'root'
})
export class AuthService {

	constructor(private cookiesService: CookiesService, private http: HttpClient) { }

	isAuthenticate(): boolean {
		return this.cookiesService.check("app.login.user");
	}

	login(username: string, password: string) {

		let headers = new HttpHeaders().set("Content-Type", "application/x-www-form-urlencoded");
		let params = new HttpParams().set('username', username).set("password", password).set("grant_type", "password");

		return this.http.post<any>(`${environment.baseUrl}/token`, params.toString(), { headers: headers }).pipe(map(user => {
			// login successful if there's a jwt token in the response
			if (user && user.access_token) {
				// store user details and jwt token in local storage to keep user logged in between page refreshes
				this.cookiesService.setObject("app.login.user", user);
			}
			return user;
		}));
	}

	logout() {
		// remove user from local storage to log user out
		this.cookiesService.deleteAll();
	}

}
