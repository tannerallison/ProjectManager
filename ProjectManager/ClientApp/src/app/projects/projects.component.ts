import {Component} from '@angular/core';
import {Project} from "../models/project.model";
import {ProjectService} from "../services/project.service";

@Component({
    selector: 'app-projects',
    templateUrl: './projects.component.html',
    styleUrls: ['./projects.component.css']
})
export class ProjectsComponent {
    projects: Project[] = [];
    projectToEdit: Project | null = null;

    constructor(private projectService: ProjectService) {

    }

    ngOnInit() {
        this.retrieveProjects();
    }

    private retrieveProjects(refresh: boolean = true) {
        this.projectService.getProjects()
            .subscribe(projects => this.projects = projects);
    }

    editProject(project: Project) {
        this.projectToEdit = project;
    }

    deleteProject(project: Project) {
        this.projectService.deleteProject(project.id)
            .subscribe(() => this.retrieveProjects());
    }

    saveProject($event: any) {

    }

    createProject() {
        this.projectService.getNewProject()
            .subscribe(project => {
                this.projectToEdit = project;
            });
    }

    projectUpdated() {
        this.projectToEdit = null;
        this.retrieveProjects()
    }
}
