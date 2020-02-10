import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http'
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class  DataService {

  private endPointLocations = 'http://localhost:5000/api/locations';
  private endPointRoads = 'http://localhost:5000/api/roads';
  private endPointLogisticsCenter = 'http://localhost:5000/api/logisticscenter';
  public contacts: any[];
  

  constructor(private http: HttpClient) { }

  public getRoads(): Observable<any>{
    return this.http.get<any[]>(`${this.endPointRoads}`);
  }

  public getLocations(): Observable<any> {
    return this.http.get<any[]>(`${this.endPointLocations}`);
  }

  public createRoad(road: any){
    return this.http.post<boolean>(`${this.endPointRoads}`,road);
  }

  public editRoad(road: any ){
    return this.http.put<boolean>(`${this.endPointRoads}`,road);
  }

  public createLocation(location: any){
    return this.http.post<boolean>(`${this.endPointLocations}`,location);
  }

  public editLocation(location: any){
    return this.http.put<boolean>(`${this.endPointLocations}`,location);
  }

  public getLocationsCenter(): Observable<any>{
    return this.http.get<any>(`${this.endPointLogisticsCenter}`);
  }

  public calculateLocationsCenter(center: any): Observable<any>{
    return this.http.post<any>(`${this.endPointLogisticsCenter}`, center);
  }
}
