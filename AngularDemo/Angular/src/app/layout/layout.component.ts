import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
    selector: 'app-layout',
    templateUrl: './layout.component.html',
    styleUrls: ['./layout.component.scss']
})
export class LayoutComponent implements OnInit {
    title: any;

    constructor(private route: ActivatedRoute) {
        // this.title = route.snapshot.component;
    }

    ngOnInit() {
    }

}
