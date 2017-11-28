import { ObjectService } from './object.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  imageData;
  imageId = '59f9c2d8d243e740a820a139';

  constructor(private _objectService: ObjectService) { }

  ngOnInit() {
    this._objectService.getObject(this.imageId)
    .subscribe(resObjectData => this.imageData = resObjectData);
  }
}

