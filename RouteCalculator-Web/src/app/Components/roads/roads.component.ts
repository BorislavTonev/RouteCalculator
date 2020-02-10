import { Component, OnInit } from '@angular/core';
import { DataService } from 'src/app/Service/data.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';


@Component({
  selector: 'app-roads',
  templateUrl: './roads.component.html',
  styleUrls: ['./roads.component.css']
})
export class RoadsComponent implements OnInit {

  public roadsList: any[];
  public locationsList: any[];
  public selectedRoad: any;
  public startingPointId: number;
  public endingPointId: number;
  title = 'modal2';
  editProfileForm: FormGroup;

  constructor(
    public dataService: DataService,
    private fb: FormBuilder,
    private modalService: NgbModal,
    private toastr: ToastrService) { }

  ngOnInit() {
    this.loadRoads();
    this.loadLoacations();
    this.editProfileForm = this.fb.group({
      id: [0],
      name: [''],
      distance: [0.0],
      startingPoint: [0],
      endingPoint: [0]
    });
  }

  public loadRoads() {
    this.dataService.getRoads().subscribe(roads => {
      this.roadsList = roads;
    });
  }

  public loadLoacations() {
    this.dataService.getLocations().subscribe(locations => {
      this.locationsList = locations;
    });
  }

  openModal(targetModal, road) {
    this.modalService.open(targetModal, {
      centered: true,
      backdrop: 'static'
    });
    if (road) {
      this.editProfileForm.patchValue({
        id: road.id,
        name: road.name,
        distance: road.distance,
        startingPoint: road.startingPoint,
        endingPoint: road.endingPoint
      });
    }
   
  }
  onSubmit() {
    this.modalService.dismissAll();
  }

  public saveChanges() {
    if (this.editProfileForm.value.id !== 0) {
      this.dataService.editRoad(this.editProfileForm.value).subscribe(
        data => this.loadRoads(),
        err => this.toastr.error('Update failed!'),
        () => this.toastr.success('Road updated!') 
      )
    } else {
      this.dataService.createRoad(this.editProfileForm.value).subscribe(
        data => this.loadRoads(),
        err => this.toastr.error('Adding failed!'),
        () => this.toastr.success('Road added!')
      )
    }

    this.resetForm();
    
  }

  public isEditMode(): boolean {
    return this.editProfileForm.value && this.editProfileForm.value.name === '';
  }

  public resetForm() {
    this.startingPointId = undefined;
    this.endingPointId = undefined;
    this.editProfileForm = this.fb.group({
      id: [0],
      name: [''],
      distance: [0],
      startingPoint: [0],
      endingPoint: [0]
    });
    this.modalService.dismissAll();
    // modal.dismiss(
  }

  onStartingPointChange($event) {
    this.startingPointId = $event.id;
  }

  onEndingPointChange($event) {
    this.endingPointId =  $event.id
  }
}
