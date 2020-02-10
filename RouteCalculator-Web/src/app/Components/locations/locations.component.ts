import { Component, OnInit } from '@angular/core';
import { DataService } from 'src/app/Service/data.service';
import { FormGroup, FormBuilder } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';


@Component({
  selector: 'app-locations',
  templateUrl: './locations.component.html',
  styleUrls: ['./locations.component.css']
})
export class LocationsComponent implements OnInit {

  public locationsList: any[];

  title = 'modal2';
  editProfileForm: FormGroup;

  constructor(public dataService: DataService,
    private modalService: NgbModal,
    private toastr: ToastrService,
    private fb: FormBuilder) { }

  ngOnInit() {
    this.loadLoacations();

    this.editProfileForm = this.fb.group({
      id: [0],
      name: [''],
      distance: [0.0],
      startingPoint: [0],
      endingPoint: [0]
    });
  }

  public loadLoacations() {
    this.dataService.getLocations().subscribe(locations => {
      this.locationsList = locations;
    });
  }

  public temp(id: any) {
    const temp3 = '';
  }

  openModal(targetModal, location) {
    this.modalService.open(targetModal, {
      centered: true,
      backdrop: 'static'
    });
    if (location) {
      this.editProfileForm.patchValue({
        id: location.id,
        name: location.name
      });
    }

  }
  
  onSubmit() {
    this.modalService.dismissAll();
  }

  public isEditMode(): boolean {
    return this.editProfileForm.value && this.editProfileForm.value.name === '';
  }

  public resetForm() {
    this.editProfileForm = this.fb.group({
      id: [0],
      name: ['']
    });
    this.modalService.dismissAll();
  }

  public saveChanges() {
    if (this.editProfileForm.value.id !== 0) {
      this.dataService.editLocation(this.editProfileForm.value).subscribe(
        data => this.loadLoacations(),
        err => this.toastr.error('Update failed!'),
        () => this.toastr.success('Location updated!')
      )
    } else {
      this.dataService.createLocation(this.editProfileForm.value).subscribe(
        data => this.loadLoacations(),
        err => this.toastr.error('Adding failed!'),
        () => this.toastr.success('Location added!')
      )
    }
    this.resetForm();
  }

}
