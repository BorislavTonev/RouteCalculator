import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { DataService } from 'src/app/Service/data.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  public logisticsCenters: any[];

  constructor(public dataService: DataService,
    private toastr: ToastrService) { }

  ngOnInit() {
    this.getCenter();
   
  }

  public calculate() {
    this.dataService.calculateLocationsCenter('').subscribe(
      data =>  this.handeResponse(data),
      err => this.toastr.error('Calculating failed!')
    )
  }

  private handeResponse(data: any) {
    const temp = data;
    if (data.status === false) {
     this.toastr.error(data.message);
    } else {
      this.toastr.success(data.message);
    }

    this.getCenter()
  }

  private getCenter() {
    this.dataService.getLocationsCenter().subscribe(center => {
      this.logisticsCenters = center;
    });
  }

}
