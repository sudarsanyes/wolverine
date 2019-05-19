import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Project, Group, Card } from '../Contracts/Contracts';
import { projection } from '@angular/core/src/render3';

@Injectable()
export class ProjectService {

    constructor(private http: HttpClient) {
    }

    public load(id: string) {
        return this.http.get("https://localhost:44314/api/projects/download/" + id);
    }

    public create(name: string) {
        return this.http.get("https://localhost:44314/api/projects/create/" + name, { responseType: 'text' });
    }

    public save(project: Project) { 
        return this.http.post("https://localhost:44314/api/projects/upload/", project);
    }
}