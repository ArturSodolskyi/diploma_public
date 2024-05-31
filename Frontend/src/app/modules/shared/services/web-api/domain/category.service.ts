import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { CreateCategoryRequestModel } from '../../../models/wep-api/domain/categories/createCategoryRequestModel';
import { UpdateCategoryRequestModel } from '../../../models/wep-api/domain/categories/updateCategoryRequestModel';


@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  private apiUrl = environment.apiUrl + 'Category/';

  constructor(private httpClient: HttpClient) { }

  public create(model: CreateCategoryRequestModel): Observable<number> {
    return this.httpClient.post<number>(this.apiUrl + 'Create', model);
  }

  public update(model: UpdateCategoryRequestModel) {
    return this.httpClient.put(this.apiUrl + 'Update', model);
  }

  public delete(id: number) {
    return this.httpClient.delete(this.apiUrl + 'Delete', { params: { id } });
  }
}
