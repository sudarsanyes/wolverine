import { Component } from '@angular/core';
import { ProjectService } from '../Services/project.service';
import { Project, Group, Card, Guid } from '../Contracts/Contracts';
import { Router } from '@angular/router';

@Component({
  selector: 'home',
  templateUrl: './home.component.html'
})
export class HomeComponent {
  CreateProjectName: string = 'Untitled-1';
  SortProjectId: string;
  AnalyzeProjectId: string;
  NewProject: Project;
  IsCreatingNewProject: boolean;
  IsOpeningForSorting: boolean;
  IsOpeningForAnalysis: boolean;

  constructor(private projectService: ProjectService, private router: Router) {
    this.IsCreatingNewProject = false;
    this.IsOpeningForSorting = false;
    this.IsOpeningForAnalysis = false;
    this.NewProject = new Project();
    this.NewProject.id = Guid.newGuid();
  }

  Sort() {
    this.IsOpeningForSorting = true;
    this.projectService.createSort(this.SortProjectId).subscribe((sortSessionId: string) => {
      this.router.navigateByUrl('/sort/' + sortSessionId);
      this.IsOpeningForSorting = false;
    });
  }

  Create() {
    this.IsCreatingNewProject = true;
    this.projectService.create(this.NewProject).subscribe((id: string) => {
      this.router.navigateByUrl('/create/' + id);
      this.IsCreatingNewProject = false;
    });
  }

  Analyze() {
    this.IsOpeningForAnalysis = true;
    this.router.navigateByUrl('/analyze/' + this.AnalyzeProjectId);
    this.IsOpeningForAnalysis = false;
  }
}