import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {Project} from "../models/project.model";
import {environment} from "../../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class ProjectService {

  private url = 'projects';

  constructor(private http: HttpClient) {
  }

  public getNewProject(): Observable<Project> {
    return this.http.get<Project>(`${environment.apiUrl}/${this.url}/new`);
  }

  public getProjects(): Observable<Project[]> {
    return this.http.get<Project[]>(`${environment.apiUrl}/${this.url}`);
  }

  public saveProject(project: Project): Observable<any> {
    return this.http.post(`${environment.apiUrl}/${this.url}`, project);
  }

  public deleteProject(projectId: string): Observable<any> {
    return this.http.delete(`${environment.apiUrl}/${this.url}/${projectId}`);
  }
}
