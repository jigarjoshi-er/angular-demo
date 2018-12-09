import { Component, OnInit } from '@angular/core';
import { AlertService } from '../services/alert.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

import { EnquiryViewModel } from '../models/enquiryViewModel';
import { DropdownModel } from '../models/dropdownModel';
import { AppService } from '../services/app.service';
import { first } from 'rxjs/operators';

@Component({
    selector: 'app-lead',
    templateUrl: './lead.component.html',
    styleUrls: ['./lead.component.scss']
})
export class LeadComponent implements OnInit {

    listView: boolean;
    enquiryForm: FormGroup;
    cities: DropdownModel[];
    submitted: boolean;
    states: DropdownModel[];
    countries: DropdownModel[];

    constructor(private alert: AlertService, private formBuilder: FormBuilder, private appService: AppService) { }

    // convenience getter for easy access to form fields
    get form() { return this.enquiryForm.controls; }

    ngOnInit() {
        this.submitted = false;
        this.listView = true;

        this.getLookups();

        this.enquiryForm = this.formBuilder.group({
            Date: [new Date(), Validators.required],
            Number: null,
            BusinessName: [null, Validators.required],
            ContactPerson: [null, Validators.required],
            PrimaryContactNumber: [null, Validators.required],
            OptionalContactNumber: null,
            Email: [null, Validators.email],
            BusinessAddress: [null, Validators.required],
            CityId: [null, Validators.required],
            StateId: [null, Validators.required],
            CountryId: [null, Validators.required]
        });
    }

    addNew() {
        this.enquiryForm.controls.Date.setValue(new Date());
        this.listView = false;
    }

    backToList() {
        this.enquiryForm.reset();
        this.submitted = false;
        this.listView = true;
    }

    save() {
        this.submitted = true;
        if (this.enquiryForm.invalid) {
            return;
        }

        this.appService.post("Leads", "Save", this.enquiryForm.value).pipe(first()).subscribe(data => {
            this.alert.Success("Lead saved successfully.");
        }, error => {
            this.alert.Error("An error occured ");
        });
    }

    getLookups() {
        this.appService.get<DropdownModel[]>("Leads", "GetLookups").pipe(first()).subscribe(data => {
            this.cities = data.filter(x => x.Type == "City");
            this.states = data.filter(x => x.Type == "State");
            this.countries = data.filter(x => x.Type == "Country");
        }, error => { });
    }
}