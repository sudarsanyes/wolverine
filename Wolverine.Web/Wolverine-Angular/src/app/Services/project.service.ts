import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Project, Group, Card, SimplifiedProject, Helpers, SortSession } from '../Contracts/Contracts';
import { projection } from '@angular/core/src/render3';

@Injectable()
export class ProjectService {

    constructor(private http: HttpClient) {
    }

    public load(id: string) {
        return this.http.get("https://wolverineservice.azurewebsites.net/api/projects/download/" + id);
    }

    public createFromName(name: string) {
        return this.http.get("https://wolverineservice.azurewebsites.net/api/projects/create/" + name, { responseType: 'text' });
    }

    public create(newProject: Project) {
        return this.http.post("https://wolverineservice.azurewebsites.net/api/projects/create/", newProject, { responseType: 'text' });
    }

    public save(simplifiedProject: SimplifiedProject) {
        var project = Helpers.toProject(simplifiedProject);
        return this.http.post("https://wolverineservice.azurewebsites.net/api/projects/upload/", project);
    }

    public createSort(projectId: string) {
        return this.http.get("https://wolverineservice.azurewebsites.net/api/sort/create/" + projectId, { responseType: 'text' });
    }

    public loadSort(id: string) {
        return this.http.get("https://wolverineservice.azurewebsites.net/api/sort/download/" + id);
    }

    public saveSort(sortSession: SortSession) {
        return this.http.post("https://wolverineservice.azurewebsites.net/api/sort/upload/", sortSession);
    }

    public loadResult(id: string) {
        return this.http.get("https://wolverineservice.azurewebsites.net/api/sessions/analyze/" + id);
    }
}