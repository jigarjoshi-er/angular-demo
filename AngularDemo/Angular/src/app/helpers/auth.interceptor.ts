import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';

import { LoaderService } from '../services/loader.service';
import { AuthService } from '../auth/auth.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
    reqestCount: number = 0;
	/**
	 *
	 */
    constructor(private loader: LoaderService, private authService: AuthService) {

    }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        // add authorization header with jwt token if available
        this.reqestCount++;
         this.loader.show();
        let token = this.authService.getToken();

        if (token) {
            request = request.clone({
                setHeaders: {
                    Authorization: `Bearer ${token}`
                }
            });
        }

        return next
            .handle(request)
            .pipe(
                tap(event => {
                    //nothing is printed when a Http failure occurs
                    if (event instanceof HttpResponse) {
                        if (--this.reqestCount === 0) {
                            this.loader.hide();
                        }
                    }
                }, (error: any) => {
                    if (--this.reqestCount === 0) {
                        this.loader.hide();
                    }
                    if (error && error.status === 401) {
                        // let toast: Toast = this.toastCtrl.create({ message: "Your session is expired. Please login again.", duration: 5000 });
                        // toast.present();
                        // this.events.publish('user:logout');
                    }
                }));
    }
}