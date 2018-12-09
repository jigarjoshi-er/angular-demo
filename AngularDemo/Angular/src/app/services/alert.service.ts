import { Injectable } from '@angular/core';

declare var $: any;

@Injectable({
    providedIn: 'root'
})
export class AlertService {

    constructor() { }

    Success(message: string) {
        this.showAlert("Success !", message, "success");
    }

    Error(message: string) {
        this.showAlert("Oops !", message, "error");
    }

    private showAlert(title: string, message: string, type: string) {
        $.toast({
            heading: title,
            text: message,
            position: 'top-right',
            icon: type,
            hideAfter: 3000,
            stack: 1,
            loader: false
        });
    }
}