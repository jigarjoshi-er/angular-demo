import { Injectable } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';

@Injectable({
	providedIn: 'root'
})
export class CookiesService {

	constructor(private cookieService: CookieService) { }

	//set Cookie 
	setObject(key: string, value: any) {
		this.cookieService.set(key, JSON.stringify(value));
	}

	//set Cookie
	set(key: string, value: string) {
		this.cookieService.set(key, JSON.stringify(value));
	}

	//get Cookie
	get(key: string) {
		return this.cookieService.get(key);
	}


	//get Cookie
	getObject(key: string) {
		return JSON.parse(this.cookieService.get(key));
	}

	//check Cookie
	check(key: string) {
		return this.cookieService.check(key);
	}

	//delete cookie
	//Syntax - delete( name: string, path?: string, domain?: string ): void;
	delete(key: string) {
		return this.cookieService.delete(key);
	}

	//delete All cookie
	//Syntax - deleteAll( path?: string, domain?: string ): void;
	deleteAll() {
		this.cookieService.deleteAll();
	}
}
