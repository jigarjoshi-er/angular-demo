import { Injectable } from '@angular/core';

declare var $: any;

@Injectable({
    providedIn: 'root'
})
export class LoaderService {

    show() {
        $("#wait").show();
    }
    hide() {
        $("#wait").hide();
    }
}