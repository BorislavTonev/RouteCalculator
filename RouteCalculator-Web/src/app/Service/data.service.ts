import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http'
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class  DataService {

  private endPointLocations = 'http://localhost:5000/api/locations';
  private endPointRoads = 'http://localhost:5000/api/roads';
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

  // public createLocation(contact: {id, name, description, email}){
  //   this.contacts.push(contact);
  // }

  // public editLocation(contact: {id, name, description, email}){
  //   this.contacts.push(contact);
  // }
}
