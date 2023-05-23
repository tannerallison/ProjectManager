import {Component, EventEmitter, Input, Output} from '@angular/core';
import {Project} from "../models/project.model";
import {ProjectService} from "../services/project.service";

@Component({
  selector: 'app-project-edit',
  templateUrl: './project-edit.component.html',
  styleUrls: ['./project-edit.component.css']
})
export class ProjectEditComponent {
  @Input() project: Project | null = null;
  @Output() projectUpdated = new EventEmitter<boolean>();

  constructor(private projectService: ProjectService) {
  }

  save() {
    this.projectService.saveProject(this.project!).subscribe(() => {
      this.projectUpdated.emit(true);
    });
  }
}
