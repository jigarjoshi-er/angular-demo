import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'AngularDemo';
  student;

  ngOnInit(): void {
    this.student = { Name: "Jigar", DateOfBirth: "09/06/1992", Gender: "Male" };
  }
}
