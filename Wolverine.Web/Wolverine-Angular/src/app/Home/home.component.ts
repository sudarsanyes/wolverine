import { Component } from '@angular/core';
import { ProjectService } from '../Services/project.service';
import { Project, Group, Card } from '../Contracts/Contracts';
import { Router } from '@angular/router';

@Component({
  selector: 'home',
  templateUrl: './home.component.html'
})
export class HomeComponent {
  Title: string = 'Wolverine.Agular';
  ProjectName: string = 'Untitled-1';
  ActiveProject: Project;

  constructor(private projectService: ProjectService, private router: Router) {
  }

  Open() {
    this.projectService.load(this.ProjectName).subscribe((data: Project) => {
      this.ActiveProject = data;
      this.router.navigateByUrl('/sort');
    });
  }

  Create() {
    this.projectService.create(this.ProjectName).subscribe((id: string) => {
      this.router.navigateByUrl('/create/' + id);
    });
  }

  Analyze() {
  }
}