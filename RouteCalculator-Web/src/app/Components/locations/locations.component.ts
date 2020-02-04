import { Component, OnInit } from '@angular/core';
import { DataService } from 'src/app/Service/data.service';

@Component({
  selector: 'app-locations',
  templateUrl: './locations.component.html',
  styleUrls: ['./locations.component.css']
})
export class LocationsComponent implements OnInit {

  contacts;
  selectedContact;

  constructor(public dataService: DataService) { }

  ngOnInit() {
     this.dataService.getLocations().subscribe( locations =>{
      this.contacts = locations;
    });    
  }
  public selectContact(contact){
    this.selectedContact = contact;
  }

}
